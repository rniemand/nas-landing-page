<script lang="ts">
	import { Col, Row } from 'sveltestrap';
	import { UserLinkDto, UserLinksClient } from '../../nlp-api';
	import HorizontalList from '../common/HorizontalList.svelte';
	import HorizontalListEntry from '../common/HorizontalListEntry.svelte';
	import LinksDisplay from './LinksDisplay.svelte';

	let categories: string[] = [];
	let links: UserLinkDto[] = [];
	let displayLinks: UserLinkDto[] = [];
	let selectedCategory: string = '';

	const selectedCategoryChanged = (_category: string) =>
		(displayLinks = links.slice().filter((x) => x.linkCategory === _category));

	(async () => {
		links = await new UserLinksClient().getUserLinks();
		categories = links.reduce((pv: string[], cv) => {
			if (pv.indexOf(cv.linkCategory) === -1) pv.push(cv.linkCategory);
			return pv;
		}, []);
		selectedCategory = categories.length === 0 ? '' : categories[0];
	})();

	$: selectedCategoryChanged(selectedCategory);
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
		<LinksDisplay links={displayLinks} />
	</Col>
</Row>
