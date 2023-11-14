<script lang="ts">
	import { Button, Icon, Input } from 'sveltestrap';
	import { BasicSearchRequest, ShoppingListClient } from '../../../nlp-api';

	export let value: string = '';
	export let allowAllOption: boolean = false;
	export let className: string = '';
	export let clearButton: boolean = false;
	let options: string[] = [];

	export const refreshCategories = async () => {
		options = await new ShoppingListClient().getCategorySuggestions(
			new BasicSearchRequest({
				filter: ''
			})
		);
	};

	refreshCategories();
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
