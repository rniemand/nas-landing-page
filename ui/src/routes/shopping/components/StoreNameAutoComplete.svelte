<script lang="ts">
	import { AutoCompleteSuggestion } from '../../../components/common/AutoComplete';
	import AutoCompleteBase from '../../../components/common/AutoCompleteBase.svelte';
	import { BasicSearchRequest, ShoppingListClient } from '../../../nlp-api';

	export let value: string | undefined = undefined;
	export let placeholder: string | undefined = undefined;
	export let showClear: boolean = false;

	const getSuggestions = async (term: string) => {
		const results = await new ShoppingListClient().getStoreNameSuggestions(
			new BasicSearchRequest({
				filter: term,
				includeCompletedEntries: true
			})
		);
		return results.map((e: string) => new AutoCompleteSuggestion(e, e));
	};
</script>

<AutoCompleteBase {getSuggestions} {showClear} bind:value {placeholder} />
