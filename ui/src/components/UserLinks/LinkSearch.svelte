<script lang="ts">
  export let onSearchChange: (term: string) => void;
  export let onClearSearch: () => void;
  let searchValue: string = '';

  const onClearSearchHandler = () => {
    searchValue = '';
    onClearSearch();
  };

  $: onSearchChange(searchValue);
</script>

<input type="text" placeholder="Search..." class="input input-bordered mr-1" bind:value={searchValue} />
<button type="button" class="btn btn-outline btn-warning" disabled={(searchValue?.length || 0) === 0} on:click={onClearSearchHandler}>Clear</button>