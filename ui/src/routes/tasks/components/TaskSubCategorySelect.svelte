<script lang="ts">
	import { Input } from 'sveltestrap';
	import { BasicSearchRequest, UserTasksClient } from '../../../nlp-api';

	export let category: string | undefined = undefined;
	export let value: string | undefined = undefined;
	export let allowAllOption: boolean = false;
	export let includeCompletedEntries: boolean = false;
	let subCategories: string[] = [];

	const refreshSubCategories = async (_category: string | undefined) => {
		subCategories = [];
		if (!_category) return;

		subCategories = await new UserTasksClient().getTaskSubCategories(
			new BasicSearchRequest({
				filter: _category,
				includeCompletedEntries: includeCompletedEntries
			})
		);
		if (value) return;
		value = allowAllOption ? undefined : subCategories[0];
	};

	$: refreshSubCategories(category);
</script>

<Input type="select" class="me-2" bind:value>
	{#if allowAllOption}
		<option value={undefined}>All</option>
	{/if}
	{#each subCategories as sub}
		<option value={sub}>{sub}</option>
	{/each}
</Input>
