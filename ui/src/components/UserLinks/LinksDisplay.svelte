<script lang="ts">
	import type { UserLinkDto } from '../../nlp-api';
  import LinkCategories from './LinkCategories.svelte';
  import LinkEntry from './LinkEntry.svelte';
  import LinkSearch from './LinkSearch.svelte';

	export let links: UserLinkDto[];
	
	const linkLookup: { [key:string]:UserLinkDto } = links.reduce((pv: { [key:string]:UserLinkDto }, cv) => {
		let key = `${cv.linkName}|${cv.linkCategory}|${cv.linkImage}`.toLowerCase();
		pv[key] = cv;
		return pv;
	}, {});
	
	const categorizeLinks = (links: UserLinkDto[]): { [key: string]: UserLinkDto[] } =>
		links.reduce((pv: { [key: string]: UserLinkDto[] }, cv) => {
			if (pv[cv.linkCategory] === void 0) pv[cv.linkCategory] = [];
			pv[cv.linkCategory].push(cv);
			return pv;
		}, {});

	const searchKeys = Object.keys(linkLookup);
	const categories = categorizeLinks(links);
	const categoryNames = Object.keys(categories);
	categoryNames.sort();
	let selectedCategory = categoryNames[0];
	let lastCategory = '';
	let currentLinks: UserLinkDto[] = links.filter(x => x.linkCategory === selectedCategory);
	
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
</script>

<style>
	.links {
		display: flex;
		padding: 6px;
		flex-direction: row;
		justify-content: center;
		flex-wrap: wrap;
	}
	h2 {
		text-align: center;
		margin: 12px 2px;
		font-size: 2em;
		text-transform: capitalize;
	}
</style>

<LinkCategories categories={categoryNames} onCategorySelected={onCategorySelectedHandler} {selectedCategory} />
<LinkSearch onSearchChange={onSearchChangeHandler} onClearSearch={onClearSearchHandler} />

<div class="link-category">
	<h2>{selectedCategory}</h2>
	<div class="links">
		{#each currentLinks as link}
			<LinkEntry {link} />
		{/each}
	</div>
</div>