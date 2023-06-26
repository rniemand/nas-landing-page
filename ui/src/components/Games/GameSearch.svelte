<style>
  .search {
    text-align: center;
    margin-bottom: 12px;
    margin-top: 6px;
    display: flex;
    padding: 6px;
  }
  input {
    border-radius: 8px;
    padding: 5px;
    border: 1px solid #6f6f6f;
    background-color: #272626;
    color: #fff;
    flex: auto;
    margin-right: 4px;
    font-size: 1.4em;
  }
  .clear {
    font-size: 1.2em;
    background-color: #7b0017;
    border-radius: 6px;
    color: #ffffff;
    border: 1px solid #6f6f6f;
    cursor: pointer;
    padding: 2px 12px;
    margin-right: 4px;
  }
  .clear:hover {
    background-color: #610112;
  }
  .add-game {
    font-size: 1.2em;
    background-color: #005306;
    border-radius: 6px;
    color: #ffffff;
    border: 1px solid #6f6f6f;
    cursor: pointer;
    padding: 2px 12px;
  }
  .add-game:hover {
    background-color: #003804;
  }
</style>

<script lang="ts">
  export let searchTermChanged: (term: string) => void;
  export let onAddGame: () => void;
  export let term = '';

  const clearSearch = () => {
    term = '';
    searchTermChanged(term);
  };
</script>

<div class="search">
  <input type="text" placeholder="Search..." bind:value={term} on:keyup={() => searchTermChanged(term)}>
  <button on:click={clearSearch} class="clear">Clear</button>
  <button class="add-game" on:click={onAddGame}>Add Game</button>
</div>