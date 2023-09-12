<script lang="ts">
	import { CategoryRequest, ContainerClient } from "../../nlp-api";
	import AutoCompleteBase from "./AutoCompleteBase.svelte";
	import { SearchResult } from "./ContainerHelper";
  export let value: string = '';
  export let placeholder: string = 'Search Categories';
  export let clearButton: boolean = false;
  export let className: string = '';
  export let onItemSelected: (item: SearchResult) => void = (item: SearchResult) => {};

  const getSuggestions = async (term: string | undefined) => {
    const itemRequest = new CategoryRequest({
      category: term || '',
      subCategory: '',
    });
    const response = await new ContainerClient().getItemCategories(itemRequest) || [];
    return response.map((cat: string) => new SearchResult(cat));
  };
</script>

<AutoCompleteBase refreshSuggestions={getSuggestions} bind:value={value} {placeholder} {clearButton} {onItemSelected} {className} />