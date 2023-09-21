<script lang="ts">
	import type { AutoCompleteSuggestion } from '../../components/common/AutoComplete';
	import AutoCompleteBase from '../../components/common/AutoCompleteBase.svelte';
	import { UserTasksClient } from '../../nlp-api';

	export let value: string | undefined = undefined;
	export let placeholder: string | undefined = undefined;
	export let showClear: boolean = false;
	const tasksClient = new UserTasksClient();

	const onSuggestionSelected = (suggestion: AutoCompleteSuggestion | undefined) => {
		console.log('onSuggestionSelected', suggestion);
	};

	const getSuggestions = async (term: string) => {
		console.log('getSuggestions', term);
		const results = await tasksClient.getTaskCategories();
		console.log('x', results);
		// const results = await new LaborRoleClient().searchRoles(term.replace(/[^a-z0-9]/gim, ' ').replace(/\s+/g, ' '));
		// return results.map((entry: LaborRoleSearchResultDto) => new AutoCompleteSuggestion(entry, entry.title));
		return [];
	};

	// $: valueChanged(value);
</script>

<AutoCompleteBase {getSuggestions} {onSuggestionSelected} {showClear} bind:value {placeholder} />
