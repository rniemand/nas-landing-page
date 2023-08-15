<style>
  .search { width: 250px; }
  .search .form-control { margin-right: 5px; }
</style>

<script lang="ts">
  export let onSearchChange: (term: string) => void;
  export let onClearSearch: () => void;

  let input: HTMLInputElement;

  const onSearchChanged = () => onSearchChange(input.value);
  const onClearSearchHandler = () => {
    input.value = '';
    onClearSearch();
  }
</script>

<div class="search d-flex">
  <input type="text" class="form-control" bind:this={input} on:keyup={onSearchChanged} placeholder="Search...">
  <button type="button" class="btn btn-outline-success" on:click={onClearSearchHandler}>Clear</button>
</div>