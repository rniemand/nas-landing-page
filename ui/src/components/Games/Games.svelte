<script lang="ts">
  import { BasicGameInfoDto, GamePlatformsClient, PlatformDto } from "../../nlp-api";
  import Modal from "../Modal.svelte";
	import GameInfoModal from "./GameInfoModal.svelte";
  import GamePlatforms from "./GamePlatforms.svelte";
  import GameReceiptModal from "./GameReceiptModal.svelte";
	import GameSetConsoleModal from "./GameSetConsoleModal.svelte";
  import PlatformGameList from "./PlatformGameList.svelte";

  let loading = true;
  let platforms: PlatformDto[];
  let selectedPlatform: PlatformDto | undefined;
  let selectedGame: BasicGameInfoDto;
  let receiptModal: Modal;
  let gameModal: Modal;
  let setConsoleModal: Modal;
  let gameList: PlatformGameList;

  const platformSelected = (platform: PlatformDto) => selectedPlatform = platform;
  const onModalClosed = () => gameList.refresh();

  const triggerActionHandler = (action: string, game: BasicGameInfoDto) => {
    selectedGame = game;
    if(action === 'receipt') { receiptModal.open(onModalClosed); }
    if(action === 'game-info') { gameModal.open(onModalClosed); }
    if(action === 'set-console') { setConsoleModal.open(onModalClosed); }
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
    <Modal bind:this={receiptModal}><GameReceiptModal game={selectedGame} /></Modal>
    <Modal bind:this={gameModal}><GameInfoModal game={selectedGame} /></Modal>
    <Modal bind:this={setConsoleModal}>
      <GameSetConsoleModal game={selectedGame} onLocationSet={() => setConsoleModal.close()} />
    </Modal>
    <GamePlatforms {platforms} {selectedPlatform} onPlatformSelected={platformSelected} />
    <PlatformGameList {selectedPlatform} triggerAction={triggerActionHandler} bind:this={gameList} />
  {/if}
</div>