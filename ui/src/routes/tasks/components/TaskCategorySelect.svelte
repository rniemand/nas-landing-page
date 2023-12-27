<script lang="ts">
	import { Input } from 'sveltestrap';
	import { UserTasksClient } from '../../../nlp-api';

	export let value: string | undefined = undefined;
	export let allowAllOption: boolean = false;
	let categories: string[] = [];

	const refreshOptions = async () => {
		categories = await new UserTasksClient().getAllCategories();
		if (value) return;
		value = categories[0];
	};

	refreshOptions();
</script>

<Input type="select" bind:value class="me-2">
	{#if allowAllOption}
		<option value={undefined}>All</option>
	{/if}
	{#each categories as category}
		<option value={category}>{category}</option>
	{/each}
</Input>
