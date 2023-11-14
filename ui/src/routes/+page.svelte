<script lang="ts">
	import { onMount } from 'svelte';
	import type { WhoAmIResponse } from '../nlp-api';
	import { authContext } from '../utils/AppStore';
	import { Button, Col, Row } from 'sveltestrap';
	import { goto } from '$app/navigation';
	import ItemButtons from '../components/core/ItemButtons.svelte';
	import ItemButton from '../components/core/ItemButton.svelte';
	import {
		ChoreUrls,
		ConfigUrls,
		GamesUrls,
		LinkUrls,
		ShoppingUrls,
		TasksUrls
	} from '../enums/AppUrls';
	import NavigationCrumbs from '../components/core/NavigationCrumbs.svelte';
	import NavigationCrumb from '../components/core/NavigationCrumb.svelte';
	let user: WhoAmIResponse | undefined;

	onMount(() => {
		return authContext.subscribe((_whoAmI: WhoAmIResponse | undefined) => {
			user = _whoAmI;
		});
	});
</script>

<NavigationCrumbs>
	<NavigationCrumb icon="bi-house-fill" />
	<NavigationCrumb title="Home" />
</NavigationCrumbs>

<Row>
	{#if user?.signedIn}
		<Col>
			<ItemButtons>
				<ItemButton small icon="bi-check2-circle" name="Chores" url={ChoreUrls.Root} />
				<ItemButton small icon="bi-gear-fill" name="Config" url={ConfigUrls.Root} />
				<ItemButton small icon="bi-controller" name="Games" url={GamesUrls.Root} />
				<ItemButton small icon="bi-link-45deg" name="Links" url={LinkUrls.Root} />
				<ItemButton small icon="bi-cart" name="Shopping" url={ShoppingUrls.Root} />
				<ItemButton small icon="bi-check-all" name="Tasks" url={TasksUrls.Root} />
			</ItemButtons>
		</Col>
	{:else}
		<Col class="text-center">
			<h1>Nas Landing Page</h1>
			<p>You are logged in</p>
			<Button color="primary" on:click={() => goto('/login')}>Login</Button>
		</Col>
	{/if}
</Row>
