<script lang="ts">
	import { Col, Row } from 'sveltestrap';
	import { UserLinkDto, UserLinksClient } from '../../nlp-api';
	import HorizontalList from '../common/HorizontalList.svelte';
	import HorizontalListEntry from '../common/HorizontalListEntry.svelte';

	let categories: string[] = [];
	let links: UserLinkDto[] = [];
	let selectedCategory: string = '';

	(async () => {
		links = await new UserLinksClient().getUserLinks();
		categories = links.reduce((pv: string[], cv) => {
			if (pv.indexOf(cv.linkCategory) === -1) pv.push(cv.linkCategory);
			return pv;
		}, []);
		selectedCategory = categories.length === 0 ? '' : categories[0];
	})();
</script>

<Row>
	<Col>
		<HorizontalList>
			{#each categories as category}
				<HorizontalListEntry active={selectedCategory === category}>
					{category}
				</HorizontalListEntry>
			{/each}
		</HorizontalList>
		<p>User links to be rendered here.</p>
	</Col>
</Row>
