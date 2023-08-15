<script lang="ts">
  export let categories: string[];
  export let onCategorySelected: (category: string) => void;
  export let selectedCategory: string;
</script>

<ul class="nav nav-tabs">
  {#each categories as category}
    <!-- svelte-ignore a11y-click-events-have-key-events -->
    <li class="nav-item">
      <a class="nav-link" href="#!" class:active={selectedCategory===category} on:click={() => onCategorySelected(category)}>{category}</a>
    </li>
  {/each}
</ul>