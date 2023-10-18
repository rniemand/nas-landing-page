<script lang="ts">
	import { Accordion, AccordionItem, Button, Col, Row } from 'sveltestrap';
	import { AppUrls, ConfigUrls } from '../../../enums/AppUrls';
	import HomeFloorSelector from '../../../components/core/HomeFloorSelector.svelte';
	import { CoreClient, type HomeRoomDto } from '../../../nlp-api';
	import AddRoomModal from './AddRoomModal.svelte';
	import { page } from '$app/stores';
	import { goto } from '$app/navigation';
	import EditRoomModal from './EditRoomModal.svelte';
	import NavigationCrumbs from '../../../components/core/NavigationCrumbs.svelte';
	import NavigationCrumb from '../../../components/core/NavigationCrumb.svelte';

	// TODO: [COMPLETE] make use of the correct home id here
	const homeID: number = 1;
	let floorId: number = 0;
	let loading: boolean = true;
	let rooms: HomeRoomDto[] = [];
	let editModal: EditRoomModal;

	const refreshRooms = async (_floorID: number) => {
		loading = true;
		rooms = (await new CoreClient().getFloorRooms(_floorID)) || [];
		loading = false;
	};

	const onRoomAdded = () => refreshRooms(floorId);
	const onRoomUpdated = () => refreshRooms(floorId);

	$: refreshRooms(floorId);
	$: floorId = parseInt($page.url?.searchParams.get('floorId') || '0');
</script>

<NavigationCrumbs>
	<NavigationCrumb icon="bi-house-fill" url={AppUrls.Home} />
	<NavigationCrumb icon="bi-gear-fill" url={ConfigUrls.Root} />
	<NavigationCrumb title="Rooms" />
</NavigationCrumbs>

<Row>
	<Col>
		<HomeFloorSelector className="mt-3" homeId={homeID} bind:value={floorId} />
		<div class="text-end mt-3">
			<Button color="primary" on:click={() => goto(ConfigUrls.Floors)}>Floors</Button>
			<AddRoomModal disabled={loading} {floorId} {onRoomAdded} />
			<EditRoomModal disabled={loading} bind:this={editModal} {onRoomUpdated} />
		</div>
		{#if rooms.length > 0}
			<Accordion class="mt-3">
				{#each rooms as room}
					<AccordionItem header={room.roomName}>
						{room.roomName}
						<div class="text-end">
							<Button color="success" on:click={() => editModal.editRoom(room)}>Edit</Button>
						</div>
					</AccordionItem>
				{/each}
			</Accordion>
		{/if}
	</Col>
</Row>
