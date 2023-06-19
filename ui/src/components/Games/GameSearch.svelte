<style>
  .search {
    text-align: center;
    margin-bottom: 12px;
    margin-top: 6px;
  }
  input, button {
    border-radius: 8px;
    padding: 5px;
    border: 1px solid #6f6f6f;
    background-color: #272626;
    color: #fff;
  }
</style>

<script lang="ts">
  export let searchTermChanged: (term: string) => void;
  export let term = '';

  const clearSearch = () => {
    term = '';
    searchTermChanged(term);
  };
</script>

<div class="search">
  <input type="text" placeholder="Search..." bind:value={term} on:keyup={() => searchTermChanged(term)}>
  <button on:click={clearSearch}>Clear</button>
</div>