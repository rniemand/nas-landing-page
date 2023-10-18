<script lang="ts">
	import { onMount } from 'svelte';
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
	import { authContext, updateAuthContext } from '../utils/AppStore';
	import { AuthClient, type WhoAmIResponse } from '../nlp-api';
	import { goto } from '$app/navigation';
	import { page } from '$app/stores';
	import { AuthUrls, ChoreUrls, ConfigUrls, GamesUrls, TasksUrls } from '../enums/AppUrls';

	let isOpen = false;
	let whoAmI: WhoAmIResponse | undefined;
	let pageId = '/';
	const handleUpdate = (event: any) => (isOpen = event.detail.isOpen);

	const runLogout = async () => {
		await new AuthClient().logout();
		updateAuthContext(undefined);
		goto('/');
	};

	onMount(() => {
		return authContext.subscribe((_whoAmI?: WhoAmIResponse) => {
			whoAmI = _whoAmI;
		});
	});

	$: pageId = $page.route.id || '';
</script>

<Navbar color="primary-subtle" class="shadow" expand="md">
	<NavbarBrand>NLP</NavbarBrand>
	<NavbarToggler on:click={() => (isOpen = !isOpen)} />
	<Collapse {isOpen} navbar expand="md" on:update={handleUpdate}>
		<Nav class="ms-auto" navbar>
			{#if whoAmI?.signedIn}
				<NavItem>
					<NavLink href={ChoreUrls.Root} active={pageId === ChoreUrls.Root}>Chores</NavLink>
				</NavItem>
				<NavItem>
					<NavLink href={GamesUrls.Root} active={pageId === GamesUrls.Root}>Games</NavLink>
				</NavItem>
				<NavItem>
					<NavLink href={TasksUrls.Root} active={pageId === TasksUrls.Root}>Tasks</NavLink>
				</NavItem>
				<Dropdown nav inNavbar>
					<DropdownToggle nav caret>Account</DropdownToggle>
					<DropdownMenu end>
						<!-- <DropdownItem divider /> -->
						<DropdownItem on:click={runLogout}>
							<i class="bi bi-key-fill" />
							Log Out
						</DropdownItem>
						<DropdownItem on:click={() => goto(ConfigUrls.Root)}>
							<i class="bi bi-gear-fill" />
							Configuration
						</DropdownItem>
					</DropdownMenu>
				</Dropdown>
			{:else}
				<NavItem>
					<NavLink href={AuthUrls.Login}>Login</NavLink>
				</NavItem>
			{/if}
		</Nav>
	</Collapse>
</Navbar>
