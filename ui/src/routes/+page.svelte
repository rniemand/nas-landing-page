<script lang="ts">
	import { getContext, onMount } from 'svelte';
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
	import type { NlpPlugin } from '../modals/NlpPlugin';
	import { AppContext } from '../enums/AppContext';
	let user: WhoAmIResponse | undefined;
	let plugins = getContext<NlpPlugin[]>(AppContext.Plugins);

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
				{#each plugins as plugin}
					<ItemButton small icon={plugin.icon} name={plugin.name} url={plugin.url} />
				{/each}
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
