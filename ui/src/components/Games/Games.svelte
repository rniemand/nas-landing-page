<script lang="ts">
  import { PlatformDto, PlatformsClient } from "../../nlp-api";
  import GamePlatforms from "./GamePlatforms.svelte";
  import PlatformGameList from "./PlatformGameList.svelte";

  let loading = true;
  let platforms: PlatformDto[];
  let selectedPlatform: PlatformDto | undefined;

  const onPlatformSelectedHandler = (platform: PlatformDto) => {
    selectedPlatform = platform;
  };

  (async () => {
    platforms = await new PlatformsClient().getAll();
    selectedPlatform = platforms ? platforms[0] : undefined;
    loading = false;
  })();
</script>

<div class="wrapper">
  {#if loading}
    Loading...
  {:else}
    <GamePlatforms {platforms} {selectedPlatform} onPlatformSelected={onPlatformSelectedHandler} />
    <PlatformGameList {selectedPlatform} />
  {/if}
</div>