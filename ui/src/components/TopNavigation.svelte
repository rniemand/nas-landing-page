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
	let userSignedIn: boolean = false;
	let userFirstName: string = '';
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
			userSignedIn = _whoAmI?.signedIn || false;
			userFirstName = (_whoAmI?.name || '').split(' ')[0];
		});
	});
</script>

<Navbar color="dark" dark expand="md">
	<NavbarBrand>NLP</NavbarBrand>
	<NavbarToggler on:click={() => (isOpen = !isOpen)} />
	<Collapse {isOpen} navbar expand="md" on:update={handleUpdate}>
		<Nav class="ms-auto" navbar>
			{#if userSignedIn}
				<Dropdown nav inNavbar>
					<DropdownToggle nav caret>{userFirstName}</DropdownToggle>
					<DropdownMenu end>
						<DropdownItem>Option 1</DropdownItem>
						<DropdownItem divider />
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
