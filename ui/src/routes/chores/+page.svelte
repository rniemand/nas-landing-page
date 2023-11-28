<script lang="ts">
	import { Accordion, AccordionItem, Button, Col, Row } from 'sveltestrap';
	import AddChoreModal from './modals/AddChoreModal.svelte';
	import { ChoreClient, type HomeChoreDto } from '../../nlp-api';
	import ChoreInfoDisplay from './components/ChoreInfoDisplay.svelte';
	import EditChoreModal from './modals/EditChoreModal.svelte';
	import ChorePriorityIcon from './components/ChorePriorityIcon.svelte';
	import CompleteChoreModal from './modals/CompleteChoreModal.svelte';
	import NavigationCrumbs from '../../components/core/NavigationCrumbs.svelte';
	import NavigationCrumb from '../../components/core/NavigationCrumb.svelte';
	import { AppUrls } from '../../enums/AppUrls';
	import HomeFloorSelector from '../../components/core/HomeFloorSelector.svelte';
	import HomeRoomSelector from '../../components/core/HomeRoomSelector.svelte';
	import { toastError, toastSuccess } from '../../components/ToastManager';

	let loading: boolean = true;
	let chores: HomeChoreDto[] = [];
	let editModal: EditChoreModal;
	let completeModal: CompleteChoreModal;
	let floorId: number = 0;
	let roomId: number = 0;
	let hasFilter: boolean = false;

	const refreshChores = async (_floorId: number, _roomId: number) => {
		loading = true;
		chores = await new ChoreClient().getChores(_floorId, _roomId);
		loading = false;
	};

	const onChoreAdded = () => refreshChores(floorId, roomId);
	const onChoreUpdated = () => refreshChores(floorId, roomId);
	const onChoreCompleted = () => refreshChores(floorId, roomId);

	const onEditChore = (chore: HomeChoreDto) => editModal?.editChore(chore);
	const onCompleteChore = (chore: HomeChoreDto) => completeModal?.completeChore(chore);

	const clearFilters = () => {
		floorId = 0;
		roomId = 0;
	};

	const onDeleteChore = async (chore: HomeChoreDto) => {
		var msg = `Are you sure you want to delete this chore "${chore.choreName}" - this cannot be undone`;
		if (!confirm(msg)) return;
		const response = await new ChoreClient().deleteChore(chore);

		if (!response.success) {
			toastError('Delete Chore', response.error || 'Failed to delete chore');
			return;
		}

		toastSuccess('Chore Deleted', `Deleted "${chore.choreName}"`);
		refreshChores(floorId, roomId);
	};

	$: refreshChores(floorId, roomId);
	$: hasFilter = floorId > 0 || roomId > 0;
</script>

<NavigationCrumbs>
	<NavigationCrumb icon="bi-house-fill" url={AppUrls.Home} />
	<NavigationCrumb title="Chores" />
</NavigationCrumbs>

<Row class="mt-3">
	<Col class="text-end">
		<AddChoreModal {onChoreAdded} />
		<EditChoreModal bind:this={editModal} {onChoreUpdated} />
		<CompleteChoreModal bind:this={completeModal} {onChoreCompleted} />
	</Col>
</Row>

<Row class="mt-3">
	<Col class="d-flex">
		<HomeFloorSelector className="me-2" allOption bind:value={floorId} />
		<HomeRoomSelector className="me-2" allOption {floorId} bind:value={roomId} />
		<Button color="warning" disabled={!hasFilter} on:click={clearFilters}>
			<i class="bi bi-trash3" />
		</Button>
	</Col>
</Row>

<Row class="mt-3">
	{#if chores.length > 0}
		<h2 class="text-center">{chores.length} Chore(s)</h2>
	{:else}
		<h2 class="text-center">No Chores</h2>
	{/if}
	<Accordion class="rn-accordian">
		{#each chores as chore (chore.choreId)}
			<AccordionItem>
				<span class="m-0" slot="header">
					<ChorePriorityIcon priority={chore.priority} />
					{chore.choreName}
				</span>
				<ChoreInfoDisplay {chore} {onEditChore} {onCompleteChore} {onDeleteChore} />
			</AccordionItem>
		{/each}
	</Accordion>
</Row>
