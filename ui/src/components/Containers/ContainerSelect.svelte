<script lang="ts">
	import { ContainerClient, IntSelectOptionDto } from "../../nlp-api";

  export let value: number = -1;
  export let className: string = '';
  let options: IntSelectOptionDto[] = [];
  let loading: boolean = true;

  const refreshOptions = async () => {
    loading = true;
    options = await new ContainerClient().getContainerDropdownOptions();
    loading = false;
  };

  refreshOptions();
</script>

<select class="select select-bordered w-full {className}" bind:value={value}>
  {#if loading}
  <option>Loading...</option>
  {/if}
  {#each options as option}
    <option value={option.value}>{option.title}</option>
  {/each}
</select>