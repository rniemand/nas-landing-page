<script lang="ts">
	import LinksDisplay from '../components/LinksDisplay.svelte';
	import { UserLinksClient, type UserLinkDto } from '../nlp-api';

	let links: UserLinkDto[] = [];
	let linksLoaded = false;

	new UserLinksClient().getAllLinks().then((responseLinks: UserLinkDto[]) => {
		links = responseLinks;
		linksLoaded = true;
	});
</script>

{#if !linksLoaded}
	<div>Loading...</div>
{:else}
	<LinksDisplay {links} />
{/if}
