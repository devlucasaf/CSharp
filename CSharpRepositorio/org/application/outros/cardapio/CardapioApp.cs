using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Application.System.Restaurante;

namespace Application.Outros.Cardapio
{
    public class CardapioApp : Application
    {
        private const string BG_APP = "#121417";
        private const string BG_PANEL = "#1B1F24";
        private const string BG_CARD = "#232A31";
        private const string BG_CARD_HOVER = "#2C353D";
        private const string ACCENT = "#F5A524";
        private const string ACCENT_HOVER = "#FFB74A";
        private const string TEXT_PRIMARY = "#F1F1F1";
        private const string TEXT_MUTED = "#B0B8C1";
        private const string DANGER = "#E5484D";

        private readonly Cardapio cardapio = new Cardapio();
        private readonly List<ItemPedido> pedido = new List<ItemPedido>();

        private ListBox listaPedido = null!;
        private TextBlock lblTotal = null!;
        private double total;

        // --- INÍCIO DA APLICAÇÃO ---
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            PopularCardapio();

            Window janela = new Window
            {
                Title = "Cardápio Digital - Restaurante",
                Width = 1180,
                Height = 720,
                MinWidth = 980,
                MinHeight = 620,
                Background = Cor(BG_APP),
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                Content = CriarInterface()
            };

            janela.Show();
        }

        // --- CRIA A INTERFACE PRINCIPAL DA APLICAÇÃO ---
        private Grid CriarInterface()
        {
            Grid root = new Grid { Margin = new Thickness(16) };

            root.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            root.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

            UIElement header = CriarHeader();
            Grid.SetRow(header, 0);
            root.Children.Add(header);

            Grid conteudo = new Grid { Margin = new Thickness(0, 12, 0, 0) };
            conteudo.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            conteudo.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(332) });

            UIElement centro = CriarCentro();
            Grid.SetColumn(centro, 0);
            conteudo.Children.Add(centro);

            UIElement painelPedido = CriarPainelPedido();
            Grid.SetColumn(painelPedido, 1);
            conteudo.Children.Add(painelPedido);

            Grid.SetRow(conteudo, 1);
            root.Children.Add(conteudo);

            return root;
        }

        // --- CABEÇALHO ---
        private StackPanel CriarHeader()
        {
            TextBlock lblTitulo = new TextBlock
            {
                Text = "🍽  Cardápio Digital",
                FontFamily = new FontFamily("Segoe UI"),
                FontSize = 26,
                FontWeight = FontWeights.Bold,
                Foreground = Cor(TEXT_PRIMARY)
            };

            TextBlock lblSubtitulo = new TextBlock
            {
                Text = "Escolha seus itens favoritos",
                FontFamily = new FontFamily("Segoe UI"),
                FontSize = 13,
                Foreground = Cor(TEXT_MUTED),
                Margin = new Thickness(0, 2, 0, 0)
            };

            return new StackPanel
            {
                Margin = new Thickness(8, 4, 8, 4),
                Children = { lblTitulo, lblSubtitulo }
            };
        }

        // --- PAINEL CENTRAL COM ABAS DE CATEGORIAS ---
        private TabControl CriarCentro()
        {
            TabControl tabControl = new TabControl
            {
                Background = Cor(BG_PANEL),
                BorderBrush = Brushes.Transparent,
                Foreground = Cor(TEXT_PRIMARY)
            };

            tabControl.Items.Add(CriarAba("Todos", cardapio.BuscarDisponiveis()));

            foreach (CategoriaItem categoria in Enum.GetValues<CategoriaItem>())
            {
                List<ItemCardapio> itens = cardapio.BuscarPorCategoria(categoria)
                    .Where(item => item.Disponivel)
                    .ToList();

                if (itens.Count > 0)
                {
                    tabControl.Items.Add(CriarAba(categoria.GetDescricao(), itens));
                }
            }

            return tabControl;
        }

        // --- CRIA UMA ABA DE CATEGORIA ---
        private TabItem CriarAba(string titulo, List<ItemCardapio> itens)
        {
            TextBlock cabecalho = new TextBlock
            {
                Text = titulo,
                FontWeight = FontWeights.Bold,
                Foreground = Cor(TEXT_PRIMARY),
                Margin = new Thickness(12, 5, 12, 5)
            };

            return new TabItem
            {
                Header = cabecalho,
                Background = Cor(BG_CARD),
                Content = CriarGridItens(itens)
            };
        }

        // --- GRID DE ITENS DO CARDÁPIO ---
        private ScrollViewer CriarGridItens(List<ItemCardapio> itens)
        {
            WrapPanel painel = new WrapPanel
            {
                Margin = new Thickness(8),
                Background = Cor(BG_PANEL),
                Orientation = Orientation.Horizontal
            };

            foreach (ItemCardapio item in itens)
            {
                painel.Children.Add(CriarCardItem(item));
            }

            return new ScrollViewer
            {
                Content = painel,
                Background = Cor(BG_PANEL),
                BorderBrush = Brushes.Transparent,
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled
            };
        }

        // --- CARD DE ITEM DO CARDÁPIO ---
        private Border CriarCardItem(ItemCardapio item)
        {
            TextBlock lblCategoria = new TextBlock
            {
                Text = item.Categoria.GetDescricao().ToUpperInvariant(),
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Foreground = Cor(ACCENT)
            };

            TextBlock lblNome = new TextBlock
            {
                Text = item.Nome,
                FontSize = 16,
                FontWeight = FontWeights.Bold,
                Foreground = Cor(TEXT_PRIMARY),
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(0, 5, 0, 0)
            };

            TextBlock lblDescricao = new TextBlock
            {
                Text = string.IsNullOrWhiteSpace(item.Descricao) ? "Sem descrição disponível." : item.Descricao,
                FontSize = 12,
                Foreground = Cor(TEXT_MUTED),
                TextWrapping = TextWrapping.Wrap,
                MaxHeight = 60,
                Margin = new Thickness(0, 5, 0, 0)
            };

            TextBlock lblDetalhes = new TextBlock
            {
                Text = DetalhesCurto(item),
                FontSize = 11,
                Foreground = Cor(TEXT_MUTED),
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(0, 5, 0, 0)
            };

            Grid rodape = new Grid { Margin = new Thickness(0, 10, 0, 0) };
            rodape.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            rodape.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

            TextBlock lblPreco = new TextBlock
            {
                Text = $"R$ {item.CalcularPrecoFinal():F2}",
                FontSize = 18,
                FontWeight = FontWeights.Bold,
                Foreground = Cor(ACCENT)
            };

            TextBlock lblTempo = new TextBlock
            {
                Text = $"⏱ {item.TempoPreparo} min",
                FontSize = 11,
                Foreground = Cor(TEXT_MUTED),
                VerticalAlignment = VerticalAlignment.Center
            };

            Grid.SetColumn(lblPreco, 0);
            Grid.SetColumn(lblTempo, 1);
            rodape.Children.Add(lblPreco);
            rodape.Children.Add(lblTempo);

            Button btnAdicionar = CriarBotao("+ Adicionar", ACCENT, "#121417");
            btnAdicionar.Margin = new Thickness(0, 10, 0, 0);
            btnAdicionar.HorizontalAlignment = HorizontalAlignment.Stretch;
            btnAdicionar.Click += (_, _) => AdicionarAoPedido(item);

            StackPanel conteudo = new StackPanel();
            conteudo.Children.Add(lblCategoria);
            conteudo.Children.Add(lblNome);
            conteudo.Children.Add(lblDescricao);
            conteudo.Children.Add(lblDetalhes);
            conteudo.Children.Add(rodape);
            conteudo.Children.Add(btnAdicionar);

            Border card = new Border
            {
                Width = 270,
                MinHeight = 250,
                Margin = new Thickness(7),
                Padding = new Thickness(14),
                Background = Cor(BG_CARD),
                BorderBrush = Cor("#2E3740"),
                BorderThickness = new Thickness(1),
                CornerRadius = new CornerRadius(10),
                Child = conteudo
            };

            card.MouseEnter += (_, _) => card.Background = Cor(BG_CARD_HOVER);
            card.MouseLeave += (_, _) => card.Background = Cor(BG_CARD);

            return card;
        }

        // --- DETALHES CURTOS PARA EXIBIÇÃO NO CARD ---
        private string DetalhesCurto(ItemCardapio item)
        {
            if (item is Prato prato)
            {
                return $"Ingrediente: {prato.IngredientePrincipal}  •  Tamanho: {prato.Tamanho.GetDescricao()}" +
                        $"{(prato.Vegetariano ? "  •  Veg" : "")}" +
                        $"{(prato.SemGluten ? "  •  Sem glúten" : "")}";
            }

            if (item is Bebida bebida)
            {
                return $"{bebida.VolumeMl}ml  •  {(bebida.Gelada ? "Gelada" : "Natural")}" +
                        $"{(bebida.Alcoolica ? $"  •  Álcool {bebida.TeorAlcoolico:F1}%" : "")}";
            }

            if (item is Entrada entrada)
            {
                return $"{(entrada.ParaCompartilhar ? "Para compartilhar" : "Individual")}  •  Serve {entrada.Porcoes}";
            }

            if (item is Sobremesa sobremesa)
            {
                return $"Sabor: {sobremesa.Sabor}  •  {sobremesa.Calorias} kcal" +
                        $"{(sobremesa.ContemLactose ? "  •  Lactose" : "")}" +
                        $"{(sobremesa.ContemNozes ? "  •  Nozes" : "")}";
            }

            if (item is Acompanhamento acompanhamento)
            {
                return $"Tipo: {acompanhamento.Tipo}" +
                        $"{(acompanhamento.AdicionalGratuito ? "  •  Grátis como adicional" : "")}";
            }

            return string.Empty;
        }

        // --- PAINEL DE PEDIDO ---
        private Border CriarPainelPedido()
        {
            TextBlock titulo = new TextBlock
            {
                Text = "Seu Pedido",
                FontSize = 18,
                FontWeight = FontWeights.Bold,
                Foreground = Cor(TEXT_PRIMARY),
                Margin = new Thickness(0, 0, 0, 12)
            };

            listaPedido = new ListBox
            {
                Background = Cor(BG_CARD),
                BorderBrush = Brushes.Transparent,
                Foreground = Cor(TEXT_PRIMARY)
            };

            lblTotal = new TextBlock
            {
                Text = "Total: R$ 0,00",
                FontSize = 22,
                FontWeight = FontWeights.Bold,
                Foreground = Cor(ACCENT),
                Margin = new Thickness(0, 12, 0, 10)
            };

            Button btnFinalizar = CriarBotao("Finalizar Pedido", ACCENT, "#121417");
            btnFinalizar.Click += (_, _) => FinalizarPedido();

            Button btnLimpar = CriarBotao("Limpar", BG_PANEL, TEXT_MUTED);
            btnLimpar.BorderBrush = Cor(TEXT_MUTED);
            btnLimpar.BorderThickness = new Thickness(1);
            btnLimpar.Margin = new Thickness(0, 8, 0, 0);
            btnLimpar.Click += (_, _) =>
            {
                pedido.Clear();
                AtualizarPedido();
            };

            Grid painel = new Grid();
            painel.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            painel.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            painel.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            Grid.SetRow(titulo, 0);
            painel.Children.Add(titulo);

            Grid.SetRow(listaPedido, 1);
            painel.Children.Add(listaPedido);

            StackPanel rodape = new StackPanel
            {
                Children = { lblTotal, btnFinalizar, btnLimpar }
            };

            Grid.SetRow(rodape, 2);
            painel.Children.Add(rodape);

            return new Border
            {
                Margin = new Thickness(12, 0, 0, 0),
                Padding = new Thickness(16),
                Background = Cor(BG_PANEL),
                CornerRadius = new CornerRadius(10),
                Child = painel
            };
        }

        // --- ADICIONA UM ITEM AO PEDIDO ---
        private void AdicionarAoPedido(ItemCardapio item)
        {
            ItemPedido? itemExistente = pedido.FirstOrDefault(ip => ip.Item.Id == item.Id);

            if (itemExistente != null)
            {
                itemExistente.Quantidade++;
            }
            else
            {
                pedido.Add(new ItemPedido(item, 1));
            }

            AtualizarPedido();
        }

        // --- ATUALIZA OS ITENS EXIBIDOS NO PEDIDO ---
        private void AtualizarPedido()
        {
            listaPedido.Items.Clear();

            foreach (ItemPedido itemPedido in pedido)
            {
                listaPedido.Items.Add(CriarLinhaPedido(itemPedido));
            }

            RecalcularTotal();
        }

        // --- CRIA UMA LINHA DO PAINEL DE PEDIDO ---
        private Border CriarLinhaPedido(ItemPedido itemPedido)
        {
            TextBlock lblNome = new TextBlock
            {
                Text = itemPedido.Item.Nome,
                FontSize = 13,
                FontWeight = FontWeights.Bold,
                Foreground = Cor(TEXT_PRIMARY),
                TextWrapping = TextWrapping.Wrap
            };

            TextBlock lblSubtitulo = new TextBlock
            {
                Text = $"{itemPedido.Quantidade}x  •  R$ {itemPedido.Item.CalcularPrecoFinal() * itemPedido.Quantidade:F2}",
                FontSize = 11,
                Foreground = Cor(TEXT_MUTED),
                Margin = new Thickness(0, 2, 0, 0)
            };

            StackPanel informacoes = new StackPanel
            {
                VerticalAlignment = VerticalAlignment.Center,
                Children = { lblNome, lblSubtitulo }
            };

            Button btnMenos = CriarBotaoPequeno("-");
            btnMenos.Click += (_, _) =>
            {
                if (itemPedido.Quantidade > 1)
                {
                    itemPedido.Quantidade--;
                }
                else
                {
                    pedido.Remove(itemPedido);
                }

                AtualizarPedido();
            };

            Button btnMais = CriarBotaoPequeno("+");
            btnMais.Click += (_, _) =>
            {
                itemPedido.Quantidade++;
                AtualizarPedido();
            };

            Button btnRemover = CriarBotaoPequeno("✕", DANGER);
            btnRemover.Click += (_, _) =>
            {
                pedido.Remove(itemPedido);
                AtualizarPedido();
            };

            StackPanel controles = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Right,
                Children = { btnMenos, btnMais, btnRemover }
            };

            Grid linha = new Grid();
            linha.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            linha.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

            Grid.SetColumn(informacoes, 0);
            Grid.SetColumn(controles, 1);
            linha.Children.Add(informacoes);
            linha.Children.Add(controles);

            return new Border
            {
                Padding = new Thickness(10, 8, 10, 8),
                Margin = new Thickness(2),
                Background = Cor(BG_PANEL),
                CornerRadius = new CornerRadius(6),
                Child = linha
            };
        }

        // --- RECALCULA O VALOR TOTAL DOS ITENS DO PEDIDO ---
        private void RecalcularTotal()
        {
            total = pedido.Sum(ip => ip.Item.CalcularPrecoFinal() * ip.Quantidade);
            lblTotal.Text = $"Total: R$ {total:F2}";
        }

        // --- FINALIZA O PEDIDO E EXIBE UM RESUMO DOS ITENS ---
        private void FinalizarPedido()
        {
            if (pedido.Count == 0)
            {
                MessageBox.Show("Adicione ao menos um item ao pedido antes de finalizar.", "Pedido vazio", MessageBoxButton.OK, MessageBoxImage.Warning);

                return;
            }

            StringBuilder resumo = new StringBuilder();

            foreach (ItemPedido itemPedido in pedido)
            {
                resumo.AppendLine(
                    $"• {itemPedido.Quantidade}x {itemPedido.Item.Nome,-25} " +
                    $"R$ {itemPedido.Item.CalcularPrecoFinal() * itemPedido.Quantidade:F2}"
                );
            }

            resumo.AppendLine();
            resumo.AppendLine(new string('─', 42));
            resumo.AppendLine($"TOTAL: R$ {total:F2}");

            MessageBox.Show(resumo.ToString(), "Pedido confirmado", MessageBoxButton.OK, MessageBoxImage.Information);

            pedido.Clear();
            AtualizarPedido();
        }

        // --- CRIA UM BOTÃO PERSONALIZADO ---
        private Button CriarBotao(string texto, string fundo, string corTexto)
        {
            Button botao = new Button
            {
                Content = texto,
                Background = Cor(fundo),
                Foreground = Cor(corTexto),
                FontWeight = FontWeights.Bold,
                Padding = new Thickness(14, 8, 14, 8),
                BorderThickness = new Thickness(0),
                Cursor = System.Windows.Input.Cursors.Hand
            };

            botao.MouseEnter += (_, _) =>
            {
                if (fundo == ACCENT)
                {
                    botao.Background = Cor(ACCENT_HOVER);
                }
            };

            botao.MouseLeave += (_, _) => botao.Background = Cor(fundo);

            return botao;
        }

        // --- CRIA UM BOTÃO PEQUENO ---
        private Button CriarBotaoPequeno(string texto, string fundo = ACCENT)
        {
            return new Button
            {
                Content = texto,
                Width = 28,
                Height = 28,
                Margin = new Thickness(2),
                Background = Cor(fundo),
                Foreground = Cor("#121417"),
                FontWeight = FontWeights.Bold,
                BorderThickness = new Thickness(0),
                Cursor = System.Windows.Input.Cursors.Hand
            };
        }

        // --- CONVERTE UMA COR HEXADECIMAL EM BRUSH ---
        private static SolidColorBrush Cor(string hexadecimal)
        {
            return new SolidColorBrush((Color)ColorConverter.ConvertFromString(hexadecimal));
        }

        // --- POPULAÇÃO DO CARDÁPIO ---
        private void PopularCardapio()
        {
            cardapio.AdicionarItem(new Entrada(1, "Bruschetta", "Fatias de pão italiano com tomate, manjericão e azeite.", 22.90, 10, true, 2));
            cardapio.AdicionarItem(new Entrada(2, "Bolinho de Bacalhau", "Porção com 6 unidades, crocantes por fora e macios por dentro.", 34.50, 15, true, 3));
            cardapio.AdicionarItem(new Entrada(3, "Salada Caprese", "Tomate, muçarela de búfala, manjericão e azeite extra virgem.", 28.00, 8, false, 1));

            cardapio.AdicionarItem(new Prato(4, "Filé Mignon ao Molho Madeira", "Filé grelhado ao ponto, molho madeira e batatas rústicas.", 89.90, 25, "Filé Mignon", false, true, TamanhoPorcao.Media));
            cardapio.AdicionarItem(new Prato(5, "Risoto de Cogumelos", "Arroz arbóreo com mix de cogumelos frescos e parmesão.", 62.00, 20, "Arroz arbóreo", true, true, TamanhoPorcao.Media));
            cardapio.AdicionarItem(new Prato(6, "Salmão Grelhado", "Salmão grelhado com legumes salteados e purê de batata baroa.", 78.50, 22, "Salmão", false, true, TamanhoPorcao.Media));
            cardapio.AdicionarItem(new Prato(7, "Lasanha à Bolonhesa", "Camadas de massa fresca, molho bolonhesa e queijos gratinados.", 54.00, 30, "Massa fresca", false, false, TamanhoPorcao.Grande));

            cardapio.AdicionarItem(new Acompanhamento(8, "Batata Frita", "Porção generosa de batata frita crocante.", 18.00, 10, "Fritura", false));
            cardapio.AdicionarItem(new Acompanhamento(9, "Arroz Branco", "Arroz soltinho preparado na hora.", 0.00, 8, "Cereal", true));
            cardapio.AdicionarItem(new Acompanhamento(10, "Legumes Grelhados", "Mix de legumes frescos grelhados com ervas.", 14.50, 12, "Vegetal", false));

            cardapio.AdicionarItem(new Sobremesa(11, "Petit Gateau", "Bolinho quente de chocolate com sorvete de creme.", 26.00, 12, 420, true, false, "Chocolate"));
            cardapio.AdicionarItem(new Sobremesa(12, "Cheesecake de Frutas Vermelhas", "Cheesecake cremoso com calda de frutas vermelhas.", 22.50, 5, 380, true, false, "Frutas Vermelhas"));
            cardapio.AdicionarItem(new Sobremesa(13, "Pudim de Leite", "Pudim tradicional com calda de caramelo.", 16.00, 5, 310, true, false, "Baunilha"));

            cardapio.AdicionarItem(new Bebida(14, "Suco de Laranja", "Suco natural feito na hora.", 12.00, 4, 400, true, false, 0));
            cardapio.AdicionarItem(new Bebida(15, "Refrigerante Lata", "Coca-Cola, Guaraná ou Sprite.", 8.00, 2, 350, true, false, 0));
            cardapio.AdicionarItem(new Bebida(16, "Água Mineral", "Com ou sem gás.", 5.00, 1, 500, true, false, 0));
            cardapio.AdicionarItem(new Bebida(17, "Chopp Pilsen", "Tulipa de chopp gelado.", 14.00, 3, 300, true, true, 4.8));
            cardapio.AdicionarItem(new Bebida(18, "Taça de Vinho Tinto", "Vinho tinto seco da casa.", 32.00, 3, 150, false, true, 12.5));
            cardapio.AdicionarItem(new Bebida(19, "Caipirinha", "Cachaça, limão, açúcar e gelo.", 22.00, 5, 300, true, true, 15.0));
        }

        // --- RESPONSÁVEL POR INICIAR A APLICAÇÃO ---
        [STAThread]
        public static void Main()
        {
            CardapioApp aplicacao = new CardapioApp();
            aplicacao.Run();
        }
    }
}
