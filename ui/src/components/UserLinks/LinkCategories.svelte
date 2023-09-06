<script lang="ts">
  export let categories: string[];
  export let onCategorySelected: (category: string) => void;
  export let selectedCategory: string;
</script>

<div class="mt-2">
  <ul class="menu menu-vertical lg:menu-horizontal bg-base-200 rounded-box">
    {#each categories as category}
      <li class="nav-item">
        <a href="#!" class:active={selectedCategory===category} on:click={() => onCategorySelected(category)}>{category}</a>
      </li>
    {/each}
  </ul>
</div>