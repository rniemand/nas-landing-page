<script lang="ts">
	import { onMount } from "svelte";
  import { BasicGameInfoDto, GamePlatformsClient, GamePlatformDto } from "../../nlp-api";
	import GameAddModal from "./GameAddModal.svelte";
	import GameInfoModal from "./GameInfoModal.svelte";
  import GamePlatforms from "./GamePlatforms.svelte";
  import GameReceiptModal from "./GameReceiptModal.svelte";
	import GameSetConsoleModal from "./GameSetConsoleModal.svelte";
  import PlatformGameList from "./PlatformGameList.svelte";

  let loading = true;
  let platforms: GamePlatformDto[];
  let selectedPlatform: GamePlatformDto | undefined;
  let selectedGame: BasicGameInfoDto;
  let gameList: PlatformGameList;
  // let genericModal: BSModal;
  let modalType: string = '';
  let modalTitle: string = '';

  const MODAL_RECEIPT = 'receipt';
  const MODAL_GAME_INFO = 'game-info';
  const MODAL_ADD_GAME = 'add-game';
  const MODAL_SET_CONSOLE = 'set-console';

  const platformSelected = (platform: GamePlatformDto) => selectedPlatform = platform;
  const onModalClosed = () => gameList.refresh();

  const triggerActionHandler = (action: string, game: BasicGameInfoDto | undefined) => {
    if(game) selectedGame = game;
    modalType = action;
    if(action === MODAL_RECEIPT) modalTitle = 'Game Receipt';
    if(action === MODAL_GAME_INFO) modalTitle = 'Game Info';
    if(action === MODAL_SET_CONSOLE) modalTitle = 'Set Console';
    if(action === MODAL_ADD_GAME) modalTitle = 'Add Game';
    // genericModal.show();
  };

  const closeModal = () => {
    // genericModal?.hide();
    onModalClosed();
  };
  
  (async () => {
    platforms = await new GamePlatformsClient().getAll();
    selectedPlatform = platforms ? platforms[0] : undefined;
    loading = false;
  })();

  onMount(() => {
    // genericModal = BSModal.getOrCreateInstance('#gameReceiptModal');
    // return () => genericModal.dispose();
  });
</script>

<div class="wrapper">
  {#if loading}
    Loading...
  {:else}
    <GamePlatforms {platforms} {selectedPlatform} onPlatformSelected={platformSelected} />
    <PlatformGameList {selectedPlatform} triggerAction={triggerActionHandler} bind:this={gameList} />
  {/if}
</div>

<div class="modal modal-lg fade" id="gameReceiptModal" tabindex="-1" aria-hidden="true">
  <div class="modal-dialog modal-dialog-centered">
    <div class="modal-content">
      <div class="modal-header">
        <h1 class="modal-title fs-4">{modalTitle}</h1>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">
        {#if modalType === MODAL_RECEIPT}<GameReceiptModal game={selectedGame} onReceiptAssociated={closeModal} />{/if}
        {#if modalType === MODAL_GAME_INFO}<GameInfoModal game={selectedGame} onGameUpdated={closeModal} />{/if}
        {#if modalType === MODAL_ADD_GAME}<GameAddModal platform={selectedPlatform} onGameAdded={closeModal} />{/if}
        {#if modalType === MODAL_SET_CONSOLE}<GameSetConsoleModal game={selectedGame} onLocationSet={closeModal} />{/if}
      </div>
    </div>
  </div>
</div>