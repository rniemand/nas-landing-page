<style>
  .search {
    width: 100%;
    font-size: 1em;
    position: relative;
    display: inline-block;
  }
  .search .input { display: flex; }
  .results {
    display: none;
    position: absolute;
    top: 100%;
    left: 0;
    transform-origin: center top;
    white-space: normal;
    background: #fff;
    margin-top: 0.5em;
    width: 18em;
    border-radius: 0.2rem;
    box-shadow: 0 2px 4px 0 rgba(34,36,38,.12), 0 2px 10px 0 rgba(34,36,38,.15);
    border: 1px solid #d4d4d5;
    width: 100%;
    overflow-y: auto;
    max-height: 350px;
    z-index: 2;
  }
  .results.visible { display: block; }
  .result {
    cursor: pointer;
    display: block;
    overflow: hidden;
    font-size: 1em;
    padding: 0.8em 1.1em;
    color: rgba(0,0,0,.87);
    line-height: 1.33;
    border-bottom: 1px solid rgba(34,36,38,.1);
  }
  .result:hover { background: #ebebeb; }
  .result .title {
    margin: -0.14em 0 0;
    font-weight: 700;
    font-size: 1em;
    color: rgba(34, 34, 34, 0.85);
  }
  .result .description {
    margin-top: 0;
    font-size: .92857143em;
    color: rgba(31, 31, 31, 0.4);
  }
  .bi.clear {
    margin-left: 4px!important;
    margin-right: 4px!important;
    cursor: pointer;
    margin: auto;
    display: inline-block;
    color: rgb(226, 28, 28);
  }
  .bi.clear.disabled {
    cursor: not-allowed;
    opacity: 0.5;
    color: rgb(70, 70, 70);
  }
</style>

<script lang="ts">
	import Spinner from "../Spinner.svelte";
	import type { SearchResult } from "./ContainerHelper";

  export let value: string;
  export let placeholder: string;
  export let clearButton: boolean = false;
  export let searchOnFocus: boolean = false;
  export let refreshSuggestions: (term: string | undefined) => Promise<SearchResult[]>;
  export let onItemSelected: (item: SearchResult) => void = (item: SearchResult) => {};

  let hasResults = false;
  let loading: boolean = false;
  let results: SearchResult[] = [];
  let canSearch: boolean = false;

  const runSearch = async (term: string | undefined) => {
    if(!canSearch) return;
    loading = true;
    results = await refreshSuggestions(term);
    hasResults = results.length > 0;
    loading = false;
  };

  const selectItem = (item: SearchResult) => {
    hasResults = false;
    value = item.value || item.title;
    onItemSelected(item);
  };

  const clearInput = () => value = '';

  const onFocus = () => {
    canSearch = true;
    if(searchOnFocus) runSearch(value);
  }

  const onBlur = () => {
    canSearch = false;
    setTimeout(() => { hasResults = false; }, 100);
  };
  
  $: runSearch(value);
</script>

<div class="search">
  <div class="input">
    <input type="text" class="form-control" placeholder={placeholder} bind:value={value} on:focus={onFocus} on:blur={onBlur} />
    {#if clearButton}
      <!-- svelte-ignore a11y-click-events-have-key-events -->
      <i class="bi bi-trash3 clear" on:click={clearInput} class:disabled={value.length === 0}></i>
    {/if}
    <Spinner show={loading} />
  </div>
  <div class="results" class:visible={hasResults}>
    {#each results as result}
      <!-- svelte-ignore a11y-click-events-have-key-events -->
      <div class="result" on:click={() => selectItem(result)}>
        <div class="content">
          <div class="title">{result.title}</div>
          <div class="description">{result.description}</div>
        </div>
      </div>
    {/each}
  </div>
</div>