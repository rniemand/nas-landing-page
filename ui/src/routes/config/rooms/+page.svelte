<script lang="ts">
	import { Accordion, AccordionItem, Button, Col, Row } from 'sveltestrap';
	import { AppUrls, ConfigUrls } from '../../../enums/AppUrls';
	import HomeFloorSelector from '../../../components/core/HomeFloorSelector.svelte';
	import { RoomClient, type HomeRoomDto } from '../../../nlp-api';
	import AddRoomModal from './AddRoomModal.svelte';
	import { page } from '$app/stores';
	import { goto } from '$app/navigation';
	import EditRoomModal from './EditRoomModal.svelte';
	import NavigationCrumbs from '../../../components/core/NavigationCrumbs.svelte';
	import NavigationCrumb from '../../../components/core/NavigationCrumb.svelte';
	import RoomInfoDisplay from './RoomInfoDisplay.svelte';

	let floorId: number = 0;
	let loading: boolean = true;
	let rooms: HomeRoomDto[] = [];
	let editModal: EditRoomModal;

	const refreshRooms = async (_floorID: number) => {
		if (_floorID <= 0) {
			rooms = [];
		} else {
			loading = true;
			rooms = (await new RoomClient().getFloorRooms(_floorID)) || [];
			loading = false;
		}
	};

	const floorSelected = (a: any) => {
		let floorId = parseInt(a?.target?.value || '0');
		if (floorId === 0) return;
		goto(ConfigUrls.FloorRooms(floorId));
	};

	const onRoomAdded = () => refreshRooms(floorId);
	const onRoomUpdated = () => refreshRooms(floorId);
	const onEditRoom = (room: HomeRoomDto) => editModal.editRoom(room);
	const onViewFloor = (room: HomeRoomDto) => goto(ConfigUrls.Floor(room.floorId));

	$: if ($page.url?.searchParams?.has('floorId'))
		floorId = parseInt($page.url?.searchParams.get('floorId') || '0');
	$: refreshRooms(floorId);
</script>

<NavigationCrumbs>
	<NavigationCrumb icon="bi-house-fill" url={AppUrls.Home} />
	<NavigationCrumb icon="bi-gear-fill" url={ConfigUrls.Root} />
	<NavigationCrumb title="Rooms" />
</NavigationCrumbs>

<Row>
	<Col>
		<HomeFloorSelector className="mt-3" bind:value={floorId} on:change={floorSelected} />
		<div class="text-end mt-3">
			<Button color="primary" on:click={() => goto(ConfigUrls.Floors)}>Floors</Button>
			<AddRoomModal disabled={loading} {floorId} {onRoomAdded} />
			<EditRoomModal disabled={loading} bind:this={editModal} {onRoomUpdated} />
		</div>
		{#if rooms.length > 0}
			<Accordion class="mt-3">
				{#each rooms as room}
					<AccordionItem header={room.roomName}>
						<RoomInfoDisplay {room} {onEditRoom} {onViewFloor} />
					</AccordionItem>
				{/each}
			</Accordion>
		{/if}
	</Col>
</Row>
