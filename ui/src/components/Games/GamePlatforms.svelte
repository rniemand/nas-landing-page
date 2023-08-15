<script lang="ts">
  import type { GamePlatformDto } from "../../nlp-api";
  export let platforms: GamePlatformDto[];
  export let selectedPlatform: GamePlatformDto | undefined;
  export let onPlatformSelected: (platform: GamePlatformDto) => void;
</script>

{#if platforms}
  <ul class="nav nav-tabs">
    {#each platforms as platform}
      <li class="nav-item">
        <a class="nav-link text-capitalize" href="#!" class:active={platform.platformID === selectedPlatform?.platformID} on:click={() => onPlatformSelected(platform)}>
          {platform.platformName}
        </a>
      </li>
    {/each}
  </ul>
{/if}