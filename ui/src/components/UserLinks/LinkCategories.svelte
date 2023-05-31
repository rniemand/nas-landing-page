<script lang="ts">
  export let categories: string[];
  export let onCategorySelected: (category: string) => void;
  export let selectedCategory: string;
</script>

<style>
  .wrapper {
    display: flex;
    justify-content: space-evenly;
    border: 1px solid #6a6969;
  }
  .cat {
    display: inline-block;
    width: auto;
    margin-right: 2px;
    padding: 4px 10px;
    background-color: #383838;
    font-size: 1.2em;
    text-transform: capitalize;
    flex: auto;
    text-align: center;
    cursor: pointer;
  }
  .cat:last-child { border-right: 0; }
  .cat:hover { background-color: #6a6969; }
  .active { background-color: #063608; }
</style>

<div class="wrapper">
  {#each categories as category}
    <!-- svelte-ignore a11y-click-events-have-key-events -->
    <div class="cat" on:click={() => onCategorySelected(category)} class:active={selectedCategory===category}>
      {category}
    </div>
  {/each}
</div>