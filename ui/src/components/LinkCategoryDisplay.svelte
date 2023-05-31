<script lang="ts">
	import type { UserLinkDto } from '../nlp-api';
	import LinkEntry from './LinkEntry.svelte';

	export let name: string;
	export let links: UserLinkDto[];
</script>

<div class="link-category">
	<h2>{name}</h2>
	<div class="links">
		{#each links as link}
			<LinkEntry {link} />
		{/each}
	</div>
</div>

<style>
	.link-category {
		margin-bottom: 6px;
	}
	h2 {
		text-align: center;
		margin: 12px 2px;
		font-size: 2em;
		text-transform: capitalize;
	}
	.links {
		display: flex;
		padding: 6px;
		flex-direction: row;
		justify-content: center;
	}
</style>
