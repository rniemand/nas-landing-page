<style>
	h2 { flex: auto; text-align: left; }
	.test {
		display: flex;
		flex-direction: row;
		flex-wrap: wrap;
		justify-content: space-evenly;
	}
</style>

<script lang="ts">
	import { UserLinksClient, type UserLinkDto } from '../../nlp-api';
	import LinkCategories from './LinkCategories.svelte';
	import LinkSearch from './LinkSearch.svelte';
	import LinkEntry from './LinkEntry.svelte';

	let links: UserLinkDto[] = [];
	let linksLoaded = false;
	let linkLookup: { [key:string]:UserLinkDto } = {};
	let searchKeys: string[] = [];
	let categories: { [key: string]: UserLinkDto[] } = {};
	let categoryNames: string[] = [];
	let selectedCategory: string = '';
	let lastCategory = '';
	let currentLinks: UserLinkDto[] = [];

	const refreshLinks = () => {
		new UserLinksClient().getAllLinks().then((responseLinks: UserLinkDto[]) => {
			links = responseLinks;
			linksLoaded = true;
		});
	};

	const categorizeLinks = (links: UserLinkDto[]): { [key: string]: UserLinkDto[] } => links.reduce((pv: { [key: string]: UserLinkDto[] }, cv) => {
		if (pv[cv.linkCategory] === void 0) pv[cv.linkCategory] = [];
		pv[cv.linkCategory].push(cv);
		return pv;
	}, {});

	const processLinks = (_links: UserLinkDto[]) => {
		linkLookup = _links.reduce((pv: { [key:string]:UserLinkDto }, cv) => {
			let key = `${cv.linkName}|${cv.linkCategory}|${cv.linkImage}`.toLowerCase();
			pv[key] = cv;
			return pv;
		}, {});

		searchKeys = Object.keys(linkLookup);
		categories = categorizeLinks(_links);
		categoryNames = Object.keys(categories);
		categoryNames.sort();
		selectedCategory = categoryNames[0];
		currentLinks = _links.filter(x => x.linkCategory === selectedCategory);
	};

	const onCategorySelectedHandler = (category: string) => {
		selectedCategory = category;
		currentLinks = links.filter(x => x.linkCategory === category);
	};

	const onSearchChangeHandler = (term: string) => {
		if(selectedCategory.toLowerCase() != 'search') lastCategory = selectedCategory;
		selectedCategory = 'Search';
		const safeTerm = (term || '').toLowerCase().trim();
		currentLinks = searchKeys.reduce((pv: UserLinkDto[], cv) => {
			if(cv.indexOf(safeTerm) != -1) pv.push(linkLookup[cv]);
			return pv;
		}, []);
	};

	const onClearSearchHandler = () => {
		selectedCategory = lastCategory;
		onCategorySelectedHandler(selectedCategory);
	};

	refreshLinks();
	$: processLinks(links);
</script>

{#if !linksLoaded}
	<div>Loading...</div>
{:else}
	<LinkCategories categories={categoryNames} onCategorySelected={onCategorySelectedHandler} {selectedCategory} />
	<div class="d-flex my-2">
		<LinkSearch onSearchChange={onSearchChangeHandler} onClearSearch={onClearSearchHandler} />
	</div>
	<div class="test">
		{#each currentLinks as link}<LinkEntry {link} />{/each}
	</div>
{/if}