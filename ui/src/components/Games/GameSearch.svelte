<script lang="ts">
  export let onAddGame: () => void;
  export let value = '';

  const clearSearch = () => {
    value = '';
  };
</script>

<input type="text" placeholder="Search..." class="input input-bordered mr-1" bind:value={value} />
<button type="button" class="btn btn-outline btn-warning mr-1" disabled={(value?.length || 0) === 0} on:click={clearSearch}>Clear</button>
<button class="btn btn-success" on:click={onAddGame}>Add</button>