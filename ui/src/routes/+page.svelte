<script lang="ts">
	import { onMount } from 'svelte';
	import type { WhoAmIResponse } from '../nlp-api';
	import { authContext } from '../utils/AppStore';

	let loggedIn = true;

	onMount(() => {
		return authContext.subscribe((_whoAmI: WhoAmIResponse | undefined) => {
			loggedIn = _whoAmI?.signedIn || false;
		});
	});
</script>

<div class="text-center">
	<h1>Nas Landing Page</h1>
	{#if loggedIn}
		<p>You are logged in</p>
	{:else}
		<p>Not logged in</p>
	{/if}
</div>
