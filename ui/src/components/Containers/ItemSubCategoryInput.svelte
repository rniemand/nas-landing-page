<script lang="ts">
	import { CategoryRequest, ContainerClient } from "../../nlp-api";
	import AutoCompleteBase from "./AutoCompleteBase.svelte";
	import { SearchResult } from "./ContainerHelper";

  export let value: string = '';
  export let category: string = '';
  export let placeholder: string = 'Search SubCategories';
  export let clearButton: boolean = false;
  export let onItemSelected: (item: SearchResult) => void = (item: SearchResult) => {};
  let lastCategory: string = '';

  const getSuggestions = async (_term: string | undefined) => {
    return (await new ContainerClient().getItemSubCategories(new CategoryRequest({
      category: category,
      subCategory: _term || '',
    })) || []).map((cat: string) => new SearchResult(cat));
  };

  const onCategoryChange = (_category: string) => {
    if(_category === '') {
      value = '';
      lastCategory = '';
      return;
    }
    if(category === _category && category === lastCategory) {
      return;
    }
    value = '';
    lastCategory = _category;
  };

  $: onCategoryChange(category);
</script>

<AutoCompleteBase refreshSuggestions={getSuggestions} bind:value={value} {placeholder} {clearButton} searchOnFocus {onItemSelected} {...$$props} />