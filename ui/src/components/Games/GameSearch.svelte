<style>
  .search { width: 350px; }
  .btn-danger { margin-left: 5px; margin-right: 5px; }
</style>

<script lang="ts">
  export let searchTermChanged: (term: string) => void;
  export let onAddGame: () => void;
  export let term = '';
  let clearDisabled: boolean = true;

  const clearSearch = () => {
    term = '';
    searchTermChanged(term);
  };

  $: clearDisabled = (term?.length || 0) === 0;
</script>

<div class="search d-flex">
  <input type="text" class="form-control" placeholder="Search..." bind:value={term} on:keyup={() => searchTermChanged(term)}>
  <button on:click={clearSearch} class="btn btn-danger" disabled={clearDisabled}>Clear</button>
  <button class="btn btn-success" on:click={onAddGame}>Add</button>
</div>