<script lang="ts">
	import { Button, Icon, Input } from 'sveltestrap';
	import { BasicSearchRequest, ShoppingListClient } from '../../../nlp-api';

	export let value: string = '';
	export let allowAllOption: boolean = false;
	export let className: string = '';
	export let clearButton: boolean = false;
	export let includeBoughtEntries: boolean = false;
	let options: string[] = [];

	export const refreshStoreNames = async () => {
		options = await new ShoppingListClient().getStoreNameSuggestions(
			new BasicSearchRequest({
				filter: '',
				includeCompletedEntries: includeBoughtEntries
			})
		);
	};

	refreshStoreNames();
</script>

<div class="w-100 d-flex {className}">
	<Input type="select" bind:value>
		{#if allowAllOption}
			<option value="">All</option>
		{/if}
		{#each options as option}
			<option value={option}>{option}</option>
		{/each}
	</Input>

	{#if clearButton}
		<Button class="ms-1" color="danger" disabled={value.length === 0} on:click={() => (value = '')}>
			<Icon name="trash" />
		</Button>
	{/if}
</div>
