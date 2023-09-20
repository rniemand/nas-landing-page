<style>
	.text-center {
		text-align: center;
	}
</style>

<script lang="ts">
	import { goto } from '$app/navigation';
	import { AuthClient, type WhoAmIResponse } from '../nlp-api';
	import { authContext, updateAuthContext } from '../utils/AppStore';
  import { Button } from 'sveltestrap';

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
    <Button on:click={runLogout} color="danger">LogOut</Button>
	{:else}
		<p>Not logged in</p>
    <Button on:click={() => goto('/login')} color="success">LogIn</Button>
	{/if}
</div>
