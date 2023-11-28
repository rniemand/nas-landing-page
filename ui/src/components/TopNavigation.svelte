<script lang="ts">
	import { getContext } from 'svelte';
	import {
		Collapse,
		Navbar,
		NavbarToggler,
		NavbarBrand,
		Nav,
		NavItem,
		NavLink,
		Dropdown,
		DropdownToggle,
		DropdownMenu,
		DropdownItem
	} from 'sveltestrap';
	import { AuthClient, type WhoAmIResponse } from '../nlp-api';
	import { goto } from '$app/navigation';
	import { page } from '$app/stores';
	import { AppUrls } from '../enums/AppUrls';
	import { AppContext } from '../enums/AppContext';
	import type { NlpPlugin } from '../modals/NlpPlugin';
	import type { Writable } from 'svelte/store';

	const user = getContext<Writable<WhoAmIResponse | undefined>>(AppContext.User);
	const plugins = getContext<NlpPlugin[]>(AppContext.Plugins);
	let isOpen = false;

	const handleUpdate = (event: any) => (isOpen = event.detail.isOpen);

	const runLogout = async () => {
		await new AuthClient().logout();
		$user = undefined;
		goto('/');
	};

	$: pageId = $page.route.id || '/';
</script>

<Navbar color="primary-subtle" class="shadow" expand="md">
	<NavbarBrand>NLP</NavbarBrand>
	<NavbarToggler on:click={() => (isOpen = !isOpen)} />
	<Collapse {isOpen} navbar expand="md" on:update={handleUpdate}>
		<Nav navbar class="w-100">
			{#if $user?.signedIn}
				{#each plugins as plugin, i}
					<NavItem
						class="d-none d-md-block {i === 0 ? 'ms-auto' : ''} {pageId === plugin.url
							? 'active'
							: ''}">
						<NavLink href={plugin.url}>
							<i class="bi {plugin.icon}" />
						</NavLink>
					</NavItem>
				{/each}
				<Dropdown class="ms-auto" nav inNavbar>
					<DropdownToggle nav caret>
						<i class="bi bi-person-fill" />
					</DropdownToggle>
					<DropdownMenu end>
						<DropdownItem on:click={runLogout}>
							<i class="bi bi-key-fill" />
							Log Out
						</DropdownItem>
					</DropdownMenu>
				</Dropdown>
			{:else}
				<NavItem>
					<NavLink href={AppUrls.Login}>Login</NavLink>
				</NavItem>
			{/if}
		</Nav>
	</Collapse>
</Navbar>

<style>
	:global(.active .nav-link i) {
		color: rgb(202 254 139) !important;
	}
</style>
