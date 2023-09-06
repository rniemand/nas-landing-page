<style>
  .search button {
    width: 100px;
    margin-left: 6px;
  }
  .search .d-flex { flex: auto; }
</style>

<script lang="ts">
	import { ContainerClient, ContainerItemDto, SearchContainerItemsRequest } from "../../nlp-api";
	import Spinner from "../Spinner.svelte";
	import ItemCategoryInput from "./ItemCategoryInput.svelte";
	import ItemSubCategoryInput from "./ItemSubCategoryInput.svelte";

  let loading: boolean = false;
  let hasResults: boolean = false;
  let searchTerm: string = '';
  let category: string = '';
  let subCategory: string = '';
  let results: ContainerItemDto[] = [];
  let debounceSearch: number | undefined = undefined;

  const clearSearch = () => {
    hasResults = false;
    results = [];
    searchTerm = '';
    category = '';
    subCategory = '';
  };

  const executeSearch = async () => {
    debounceSearch = undefined;
    results = await new ContainerClient().searchContainerItems(new SearchContainerItemsRequest({
      term: searchTerm,
      category: category,
      subCategory: subCategory
    }));
    hasResults = results.length > 0;
  };

  const runSearch = async () => {
    if(debounceSearch) {
      clearTimeout(debounceSearch);
      debounceSearch = undefined;
    }
    debounceSearch = setTimeout(executeSearch, 250);
  };
</script>

<div class="flex">
  <ItemCategoryInput placeholder="Category" bind:value={category} clearButton onItemSelected={runSearch} className="flex-1" />
  <ItemSubCategoryInput placeholder="SubCategory" bind:category={category} bind:value={subCategory} clearButton onItemSelected={runSearch} className="flex-1" />
  <input type="text" class="input input-bordered flex-1 mr-2" placeholder="Item Name" bind:value={searchTerm} on:keyup={runSearch} />
  <button class="btn btn-warning" disabled={!hasResults} on:click={clearSearch}>Clear</button>
</div>

<Spinner show={loading} />
{#if hasResults}
  <div class="results">
    <h2>Search Results</h2>
    <table class="table table-striped table-hover table-bordered table-sm">
      <thead>
        <tr>
          <th scope="col" colspan="2">Container</th>
          <th scope="col" colspan="2">Categorization</th>
          <th scope="col">Qty</th>
          <th scope="col">Name</th>
          <th scope="col">&nbsp;</th>
        </tr>
      </thead>
      <tbody>
        {#each results as result}
          <tr>
            <td>{result.containerLabel}</td>
            <td>{result.containerName}</td>
            <td>{result.category}</td>
            <td>{result.subCategory}</td>
            <td>{result.quantity}</td>
            <td>{result.inventoryName}</td>
            <td>
              <a href="/containers/items/?id={result.containerId}">
                <i class="bi bi-pencil-square"></i>
              </a>
            </td>
          </tr>
        {/each}
      </tbody>
    </table>
  </div>
{/if}