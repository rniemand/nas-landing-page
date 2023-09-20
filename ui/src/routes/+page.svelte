<script lang="ts">
	import type { WhoAmIResponse } from '../nlp-api';
	import { authContext } from '../utils/AppStore';

	let loggedIn = true;

	authContext.subscribe((_whoAmI: WhoAmIResponse | undefined) => {
		loggedIn = _whoAmI?.signedIn || false;
	});
</script>

<div class="container text-center">
	{#if loggedIn}
		<p>You are logged in</p>
	{:else}
		<p>Not logged in</p>
	{/if}
</div>
