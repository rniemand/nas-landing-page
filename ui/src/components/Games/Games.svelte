<script lang="ts">
  import { BasicGameInfoDto, PlatformDto, PlatformsClient } from "../../nlp-api";
  import Modal from "../Modal.svelte";
  import GamePlatforms from "./GamePlatforms.svelte";
  import GameReceiptInfo from "./GameReceiptInfo.svelte";
  import PlatformGameList from "./PlatformGameList.svelte";

  let loading = true;
  let platforms: PlatformDto[];
  let selectedPlatform: PlatformDto | undefined;
  let selectedGame: BasicGameInfoDto;
  let receiptModal: Modal;

  const onPlatformSelectedHandler = (platform: PlatformDto) => {
    selectedPlatform = platform;
  };

  const triggerActionHandler = (action: string, game: BasicGameInfoDto) => {
    selectedGame = game;
    if(action === 'receipt') { receiptModal.open(); }
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
    <Modal bind:this={receiptModal}>
      <GameReceiptInfo game={selectedGame} />
    </Modal>
    <GamePlatforms {platforms} {selectedPlatform} onPlatformSelected={onPlatformSelectedHandler} />
    <PlatformGameList {selectedPlatform} triggerAction={triggerActionHandler} />
  {/if}
</div>