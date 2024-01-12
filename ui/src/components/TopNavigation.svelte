<script lang="ts">
	import { getContext } from 'svelte';
	import { Navbar, NavbarBrand, Nav, NavItem, NavLink } from 'sveltestrap';
	import { AuthClient, type WhoAmIResponse } from '../nlp-api';
	import { goto } from '$app/navigation';
	import { AppUrls } from '../enums/AppUrls';
	import { AppContext } from '../enums/AppContext';
	import type { Writable } from 'svelte/store';

	const user = getContext<Writable<WhoAmIResponse | undefined>>(AppContext.User);

	const runLogout = async () => {
		await new AuthClient().logout();
		$user = undefined;
		goto(AppUrls.Home);
	};
</script>

<Navbar color="primary-subtle" class="shadow" expand="md">
	<NavbarBrand>NLP</NavbarBrand>
	<Nav navbar>
		{#if $user?.signedIn}
			<NavItem>
				<NavLink href="#!" on:click={runLogout}>Log Out</NavLink>
			</NavItem>
		{:else}
			<NavItem>
				<NavLink href={AppUrls.Login}>Login</NavLink>
			</NavItem>
		{/if}
	</Nav>
</Navbar>
