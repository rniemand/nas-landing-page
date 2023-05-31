<script lang="ts">
	import type { UserLinkDto } from '../nlp-api';
	import LinkCategoryDisplay from './LinkCategoryDisplay.svelte';

	export let links: UserLinkDto[];

	const categorizeLinks = (links: UserLinkDto[]): { [key: string]: UserLinkDto[] } =>
		links.reduce((pv: { [key: string]: UserLinkDto[] }, cv) => {
			if (pv[cv.linkCategory] === void 0) pv[cv.linkCategory] = [];
			pv[cv.linkCategory].push(cv);
			return pv;
		}, {});

	const categories = categorizeLinks(links);
	const categoryNames = Object.keys(categories);
</script>

{#each categoryNames as categoryName}
	<LinkCategoryDisplay name={categoryName} links={categories[categoryName]} />
{/each}
