<script lang="ts">
  import { BasicGameInfoDto, GamePlatformsClient, PlatformDto } from "../../nlp-api";
  import Modal from "../Modal.svelte";
  import GamePlatforms from "./GamePlatforms.svelte";
  import GameReceiptModal from "./GameReceiptModal.svelte";
  import PlatformGameList from "./PlatformGameList.svelte";

  let loading = true;
  let platforms: PlatformDto[];
  let selectedPlatform: PlatformDto | undefined;
  let selectedGame: BasicGameInfoDto;
  let receiptModal: Modal;
  let gameList: PlatformGameList;

  const onPlatformSelectedHandler = (platform: PlatformDto) => {
    selectedPlatform = platform;
  };

  const onModalClosed = () => {
    gameList.refresh();
  }

  const triggerActionHandler = (action: string, game: BasicGameInfoDto) => {
    selectedGame = game;
    if(action === 'receipt') { receiptModal.open(onModalClosed); }
  };

  (async () => {
    platforms = await new GamePlatformsClient().getAll();
    selectedPlatform = platforms ? platforms[0] : undefined;
    loading = false;
  })();
</script>

<div class="wrapper">
  {#if loading}
    Loading...
  {:else}
    <Modal bind:this={receiptModal}>
      <GameReceiptModal game={selectedGame} />
    </Modal>
    <GamePlatforms {platforms} {selectedPlatform} onPlatformSelected={onPlatformSelectedHandler} />
    <PlatformGameList {selectedPlatform} triggerAction={triggerActionHandler} bind:this={gameList} />
  {/if}
</div>