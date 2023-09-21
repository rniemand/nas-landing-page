<script lang="ts">
	import { onMount } from 'svelte';
	import type { WhoAmIResponse } from '../nlp-api';
	import { authContext } from '../utils/AppStore';
	import { Button, Col, Row } from 'sveltestrap';
	import { goto } from '$app/navigation';
	import UserLinks from '../components/UserLinks/UserLinks.svelte';
	let user: WhoAmIResponse | undefined;

	onMount(() => {
		return authContext.subscribe((_whoAmI: WhoAmIResponse | undefined) => {
			user = _whoAmI;
		});
	});
</script>

{#if user?.signedIn}
	<UserLinks />
{:else}
	<Row>
		<Col class="text-center">
			<h1>Nas Landing Page</h1>
			<p>You are logged in</p>
			<Button color="primary" on:click={() => goto('/login')}>Login</Button>
		</Col>
	</Row>
{/if}
