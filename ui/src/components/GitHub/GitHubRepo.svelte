<script lang="ts">
  import type { GitHubRepoDto } from "../../nlp-api";

  export let repo: GitHubRepoDto;
</script>

<div>
  Repo Name: {repo.name}
</div>