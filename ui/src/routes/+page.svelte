<script lang="ts">
	import { getContext } from 'svelte';
	import type { WhoAmIResponse } from '../nlp-api';
	import { Alert, Button, Col, Row } from 'sveltestrap';
	import { goto } from '$app/navigation';
	import ItemButtons from '../components/core/ItemButtons.svelte';
	import ItemButton from '../components/core/ItemButton.svelte';
	import NavigationCrumbs from '../components/core/NavigationCrumbs.svelte';
	import NavigationCrumb from '../components/core/NavigationCrumb.svelte';
	import type { NlpPlugin } from '../modals/NlpPlugin';
	import { AppContext } from '../enums/AppContext';
	import type { Writable } from 'svelte/store';
	let plugins = getContext<NlpPlugin[]>(AppContext.Plugins);
	const user = getContext<Writable<WhoAmIResponse>>(AppContext.User);
</script>

<NavigationCrumbs>
	<NavigationCrumb icon="bi-house-fill" />
</NavigationCrumbs>

<Row>
	{#if $user?.signedIn}
		<Col>
			<ItemButtons>
				{#each plugins as plugin}
					<ItemButton small icon={plugin.icon} name={plugin.name} url={plugin.url} />
				{/each}
			</ItemButtons>
		</Col>
	{:else}
		<Col>
			<Alert color="info" dismissible>You are not logged in</Alert>
			<div class="text-center">
				<Button color="primary" on:click={() => goto('/login')}>Go to Login</Button>
			</div>
		</Col>
	{/if}
</Row>
