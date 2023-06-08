<script lang="ts">
  import type { GitHubRepoDto } from "../../nlp-api";
  import GitHubRepo from "./GitHubRepo.svelte";

  export let repos: GitHubRepoDto[];
</script>

<div>
  {#if repos.length === 0}
    No Repos
  {:else}
    {#each repos as repo}
      <GitHubRepo {repo} />
    {/each}
  {/if}
</div>