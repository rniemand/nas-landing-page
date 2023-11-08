<script lang="ts">
	import NavigationCrumbs from '../../../components/core/NavigationCrumbs.svelte';
	import NavigationCrumb from '../../../components/core/NavigationCrumb.svelte';
	import { AppUrls, ConfigUrls } from '../../../enums/AppUrls';
	import { HomeClient, type HomeDto } from '../../../nlp-api';
	import { Accordion, AccordionItem, Button, Col, Row } from 'sveltestrap';
	import AddHomeModal from './AddHomeModal.svelte';
	import UpdateHomeModal from './UpdateHomeModal.svelte';
	import HomeInfoDisplay from './HomeInfoDisplay.svelte';
	import { goto } from '$app/navigation';

	let homes: HomeDto[] = [];
	let loading: boolean = true;
	let updateModal: UpdateHomeModal;

	const onEdit = (home: HomeDto) => updateModal.editHome(home);
	const showFloors = () => goto(ConfigUrls.Floors);

	const refreshHomes = async () => {
		loading = true;
		homes = (await new HomeClient().listHomes()) || [];
		loading = false;
	};

	refreshHomes();
</script>

<NavigationCrumbs>
	<NavigationCrumb icon="bi-house-fill" url={AppUrls.Home} />
	<NavigationCrumb icon="bi-gear-fill" url={ConfigUrls.Root} />
	<NavigationCrumb title="Homes" />
</NavigationCrumbs>

<Row class="mt-3">
	<Col class="text-end">
		<AddHomeModal onHomeAdded={refreshHomes} />
		<UpdateHomeModal bind:this={updateModal} onHomeUpdated={refreshHomes} />
	</Col>
</Row>

<Row class="mt-3">
	<Col>
		<Accordion>
			{#each homes as home}
				<AccordionItem header={home.homeName}>
					<HomeInfoDisplay {home} {onEdit} {showFloors} />
				</AccordionItem>
			{/each}
		</Accordion>
	</Col>
</Row>
