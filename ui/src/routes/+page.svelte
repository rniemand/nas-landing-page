<script lang="ts">
	import { UserLinksClient, type UserLinkDto } from '../nlp-api';

	let links: UserLinkDto[] = [];

	new UserLinksClient().getAllLinks().then((links: UserLinkDto[]) => {
		console.log('links', links);
	});
</script>

<h1>Welcome to SvelteKit</h1>
<p>Visit <a href="https://kit.svelte.dev">kit.svelte.dev</a> to read the documentation</p>
