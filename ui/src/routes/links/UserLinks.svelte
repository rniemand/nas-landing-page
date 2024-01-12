<script lang="ts">
	import { Col, Row } from 'sveltestrap';
	import { UserLinkDto, UserLinksClient } from '../../nlp-api';
	import LinksDisplay from './LinksDisplay.svelte';
	import LinkSearch from './LinkSearch.svelte';
	import HorizontalList from '../../components/common/HorizontalList.svelte';
	import HorizontalListEntry from '../../components/common/HorizontalListEntry.svelte';

	let categories: string[] = [];
	let links: UserLinkDto[] = [];
	let displayLinks: UserLinkDto[] = [];
	let selectedCategory: string = '';
	let searchTerm: string = '';

	const selectedCategoryChanged = (_category: string) => {
		displayLinks = links.slice().filter((x) => x.linkCategory === _category);
		searchTerm = '';
	};

	const searchTermChanged = (_term: string) => {
		if (!_term || _term.length === 0) {
			displayLinks = links.slice().filter((x) => x.linkCategory === selectedCategory);
			return;
		}
		let safeTerm = _term.toLowerCase().trim();
		displayLinks = links.slice().filter((x) => x.linkName.toLowerCase().indexOf(safeTerm) !== -1);
	};

	const onLinkSelected = async (link: UserLinkDto) => {
		await new UserLinksClient().followLink(link.linkId);
		link.followCount += 1;
		displayLinks = displayLinks;
		window.open(link.linkUrl, '_blank');
	};

	(async () => {
		links = await new UserLinksClient().getUserLinks();
		categories = links.reduce((pv: string[], cv) => {
			if (pv.indexOf(cv.linkCategory) === -1) pv.push(cv.linkCategory);
			return pv;
		}, []);
		selectedCategory = categories.length === 0 ? '' : categories[0];
	})();

	$: selectedCategoryChanged(selectedCategory);
	$: searchTermChanged(searchTerm);
</script>

<Row>
	<Col>
		<HorizontalList>
			{#each categories as category}
				<HorizontalListEntry
					active={selectedCategory === category}
					on:click={() => (selectedCategory = category)}>
					{category}
				</HorizontalListEntry>
			{/each}
		</HorizontalList>
		<LinkSearch bind:searchTerm />
		<LinksDisplay links={displayLinks} {onLinkSelected} />
	</Col>
</Row>
