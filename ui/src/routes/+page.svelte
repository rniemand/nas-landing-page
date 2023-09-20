<style>
	.text-center {
		text-align: center;
	}
</style>

<script lang="ts">
	import { goto } from '$app/navigation';
	import { AuthClient, type WhoAmIResponse } from '../nlp-api';
	import { authContext, updateAuthContext } from '../utils/AppStore';

	let loggedIn = true;

	const runLogout = async () => {
		await new AuthClient().logout();
		updateAuthContext(undefined);
		goto('/');
	};

	authContext.subscribe((_whoAmI: WhoAmIResponse | undefined) => {
		loggedIn = _whoAmI?.signedIn || false;
	});
</script>

<div class="text-center">
	<h1>Nas Landing Page</h1>
	{#if loggedIn}
		<p>You are logged in</p>
		<p><a href="#!" on:click={runLogout}>LogOut</a></p>
	{:else}
		<p>Not logged in</p>
		<p><a href="/login">LogIn</a></p>
	{/if}
</div>
