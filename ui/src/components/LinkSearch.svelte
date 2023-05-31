<script lang="ts">
  export let onSearchChange: (term: string) => void;
  export let onClearSearch: () => void;

  let input: HTMLInputElement;

  const onSearchChanged = () => {
    onSearchChange(input.value);
  };

  const onClearSearchHandler = () => {
    input.value = '';
    onClearSearch();
  }
</script>

<style>
  .search {
    text-align: right;
    padding: 3px;
  }
  input {
    border: 1px solid #8f8f8f;
    padding: 3px;
    border-radius: 4px;
    margin-right: 6px;
    font-size: 1.2em;
  }
  button {
    border: 1px solid #8f8f8f;
    padding: 3px 8px;
    border-radius: 4px;
  }
</style>

<div class="search">
  <input type="text" bind:this={input} on:keyup={onSearchChanged} placeholder="Search...">
  <button type="button" on:click={onClearSearchHandler}>Clear</button>
</div>