<script lang="ts">
	import { AutoCompleteSuggestion } from '../../../components/common/AutoComplete';
	import AutoCompleteBase from '../../../components/common/AutoCompleteBase.svelte';
	import { BasicSearchRequest, UserTasksClient } from '../../../nlp-api';

	const tasksClient = new UserTasksClient();
	export let value: string | undefined = undefined;
	export let placeholder: string | undefined = undefined;
	export let category: string = '';
	export let showClear: boolean = false;
	let lastCategory: string = '';

	const onCategoryChanged = (_cat: string) => {
		if (_cat === lastCategory) return;
		value = lastCategory ? '' : value;
		lastCategory = _cat;
	};

	const getSuggestions = async (term: string) => {
		if (!category || category.length === 0) return [];
		const results = await tasksClient.getTaskSubCategories(
			new BasicSearchRequest({
				filter: category,
				subFilter: term
			})
		);
		return results.map((e: string) => new AutoCompleteSuggestion(e, e));
	};

	$: onCategoryChanged(category);
</script>

<AutoCompleteBase {getSuggestions} {showClear} bind:value {placeholder} />
