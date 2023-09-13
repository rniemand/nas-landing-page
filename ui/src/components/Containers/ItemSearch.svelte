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

<div class="flex flex-row flex-wrap my-3 p-2">
  <ItemCategoryInput placeholder="Category" bind:value={category} clearButton onItemSelected={runSearch} className="basis-1/2 md:basis-1/4" />
  <ItemSubCategoryInput placeholder="SubCategory" bind:category={category} bind:value={subCategory} clearButton onItemSelected={runSearch} className="basis-1/2 md:basis-1/4" />
  <div class="flex flex-auto mt-3 md:mt-0">
    <input type="text" class="input input-bordered flex-1 mr-2" placeholder="Item Name" bind:value={searchTerm} on:keyup={runSearch} />
    <button class="btn btn-warning" disabled={!hasResults} on:click={clearSearch}>Clear</button>
  </div>
</div>

<Spinner show={loading} />
{#if hasResults}
  <div class="results mb-3">
    <table class="table table-zebra">
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
          <tr class="hover">
            <td>{result.containerLabel}</td>
            <td>{result.containerName}</td>
            <td>{result.category}</td>
            <td>{result.subCategory}</td>
            <td>{result.quantity}</td>
            <td>{result.inventoryName}</td>
            <td>
              <a href="/containers/items/?id={result.containerId}" class="btn btn-sm btn-primary">
                <i class="bi bi-pencil-square"></i>
              </a>
            </td>
          </tr>
        {/each}
      </tbody>
    </table>
  </div>
  <div class="divider"></div> 
{/if}