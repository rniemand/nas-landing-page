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
		DropdownItem,
		Icon
	} from 'sveltestrap';
	import { authContext, updateAuthContext } from '../utils/AppStore';
	import { AuthClient, type WhoAmIResponse } from '../nlp-api';
	import { goto } from '$app/navigation';

	let isOpen = false;
	let whoAmI: WhoAmIResponse | undefined;
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
</script>

<Navbar color="primary-subtle" class="shadow" expand="md">
	<NavbarBrand>NLP</NavbarBrand>
	<NavbarToggler on:click={() => (isOpen = !isOpen)} />
	<Collapse {isOpen} navbar expand="md" on:update={handleUpdate}>
		<Nav class="ms-auto" navbar>
			{#if whoAmI?.signedIn}
				<NavItem>
					<NavLink href="/tasks">Tasks</NavLink>
				</NavItem>
				<Dropdown nav inNavbar>
					<DropdownToggle nav caret>Account</DropdownToggle>
					<DropdownMenu end>
						<!-- <DropdownItem divider /> -->
						<DropdownItem on:click={runLogout}>
							<Icon name="lock-fill" />
							Log Out
						</DropdownItem>
					</DropdownMenu>
				</Dropdown>
			{:else}
				<NavItem>
					<NavLink href="/login">Login</NavLink>
				</NavItem>
			{/if}
		</Nav>
	</Collapse>
</Navbar>
