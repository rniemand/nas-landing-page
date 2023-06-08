<style>
  .repos {
    border: 1px solid rgb(59, 58, 58);
    overflow-y: auto;
    max-height: 500px;
    margin: 6px 12px;
    border-radius: 6px;
  }
</style>

<script lang="ts">
  import type { GitHubRepoDto } from "../../nlp-api";
  import GitHubRepo from "./GitHubRepo.svelte";

  export let repos: GitHubRepoDto[];
</script>

<div class="repos">
  {#if repos.length === 0}
    No Repos
  {:else}
    {#each repos as repo}
      <GitHubRepo {repo} />
    {/each}
  {/if}
</div>